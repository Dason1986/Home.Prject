using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;

namespace Home.Repository.HOME_SQL
{
    using global::Repository;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MainBoundedContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"HOME_SQL";
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());
        }

        protected override void Seed(MainBoundedContext context)
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

    internal class CustomSqlServerMigrationSqlGenerator : SqlServerMigrationSqlGenerator
    {
        protected override void Generate(AddColumnOperation addColumnOperation)
        {
            SetCreatedUtcColumn(addColumnOperation.Column);

            base.Generate(addColumnOperation);
        }

        protected override void Generate(CreateTableOperation createTableOperation)
        {
            SetCreatedUtcColumn(createTableOperation.Columns);

            base.Generate(createTableOperation);
        }

        private static void SetCreatedUtcColumn(IEnumerable<ColumnModel> columns)
        {
            foreach (var columnModel in columns)
            {
                SetCreatedUtcColumn(columnModel);
            }
        }

        private static readonly string[] TimeColumnNames = { "Modified", "Created" };
        private static readonly string[] UserColumnNames = { "ModifiedBy", "CreatedBy" };

        private static void SetCreatedUtcColumn(ColumnModel column)
        {
            if (column.IsNullable == false && column.Type == PrimitiveTypeKind.DateTime)
            {
                if (TimeColumnNames.Any(n => string.Equals(n, column.Name, StringComparison.OrdinalIgnoreCase)))
                    column.DefaultValueSql = "getdate()";
            }
            if (UserColumnNames.Any(n => string.Equals(n, column.Name, StringComparison.OrdinalIgnoreCase)))
                column.DefaultValueSql = "'script'";
            if (column.IsNullable == false && string.Equals("Id", column.Name, StringComparison.OrdinalIgnoreCase))
            {
                if (column.Type == PrimitiveTypeKind.Guid) column.DefaultValueSql = "newid()";
                if (column.Type == PrimitiveTypeKind.Int32) column.IsIdentity = true;
            }
            if (string.Equals("StatusCode", column.Name, StringComparison.OrdinalIgnoreCase) && column.Type == PrimitiveTypeKind.Int32)
            {
                column.DefaultValueSql = "2";
            }
        }
    }
}