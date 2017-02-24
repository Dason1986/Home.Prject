using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using MySql.Data.Entity;

namespace Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    // [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    internal sealed class Configuration : DbMigrationsConfiguration<Repository.MainBoundedContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations";
            SetSqlGenerator("MySql.Data.MySqlClient", new CustomMySqlMigrationSqlGenerator());
        }

        protected override void Seed(Repository.MainBoundedContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }

    internal class CustomMySqlMigrationSqlGenerator : MySqlMigrationSqlGenerator
    {
        protected override MigrationStatement Generate(AddColumnOperation addColumnOperation)
        {
            SetCreatedUtcColumn(addColumnOperation.Column);

            return base.Generate(addColumnOperation);
        }

        protected override MigrationStatement Generate(CreateTableOperation createTableOperation)
        {
            SetCreatedUtcColumn(createTableOperation.Columns);

            return base.Generate(createTableOperation);
        }

        private void SetCreatedUtcColumn(IEnumerable<ColumnModel> columns)
        {
            foreach (var columnModel in columns)
            {
                SetCreatedUtcColumn(columnModel);
            }
        }

        private static readonly string[] TimeColumnNames = { "Modified", "Created" };
        private static readonly string[] UserColumnNames = { "ModifiedBy", "CreatedBy" };

        private void SetCreatedUtcColumn(ColumnModel column)
        {
            if (column.IsNullable == false && column.Type == PrimitiveTypeKind.DateTime)
            {
                if (TimeColumnNames.Any(n => string.Equals(n, column.Name, StringComparison.OrdinalIgnoreCase)))
                    column.DefaultValueSql = "now()";
            }
            if (UserColumnNames.Any(n => string.Equals(n, column.Name, StringComparison.OrdinalIgnoreCase)))
                column.DefaultValueSql = "'script'";
            if (column.IsNullable == false && string.Equals("Id", column.Name, StringComparison.OrdinalIgnoreCase))
            {
                if (column.Type == PrimitiveTypeKind.Guid) column.DefaultValueSql = "uuid()";
                if (column.Type == PrimitiveTypeKind.Int32) column.IsIdentity = true;
            }
            if (string.Equals("StatusCode", column.Name, StringComparison.OrdinalIgnoreCase) && column.Type == PrimitiveTypeKind.Int32)
            {
                column.DefaultValueSql = "2";
            }
        }
    }
}