using Home.DomainModel;
using NUnit.Framework;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Home.DomainModel.Aggregates.AlertAgg;
using Library.ComponentModel.Model;
using Library;
using Home.DomainModel.Aggregates.ContactAgg;

namespace RepositoryTest
{
    internal class testCreate : ICreatedInfo
    {
        public DateTime Created
        {
            get
            {
                return time;
            }
        }

        private DateTime time = new DateTime(2012, 1, 1);

        public string CreatedBy
        {
            get
            {
                return "CreateScript";
            }
        }
    }

    [TestFixture(Category = "數據庫-初始化")]
    public class Test
    {
        [Test()]
        public void TestCase()
        {
            MainBoundedContext dbcontext = new MainBoundedContext();
            var roleId = Guid.Parse("00f73871-afe7-431a-a9ec-df44b1dcb736");
            var contactRole = dbcontext.Set<FamilyRole>();

            #region MyRegion

            FamilyRole left, right;
            FamilyRelation relation;
            var crate = new testCreate();

            var contactRelation = dbcontext.Set<FamilyRelation>();
            var relationId = Guid.Parse("00bee159-a76e-4864-b151-73082da3cb18");
            // /*

            left = contactRole.Add(new FamilyRole(crate) { ID = roleId, Name = "老公", Six = Gender.Male });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "老婆",
                Six = Gender.Female,
            });
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "高祖父",
                Six = Gender.Male,
                Level = 4,
            });
            left.AddMemberAddress(new[] { MemberAddress.Father, MemberAddress.Father, MemberAddress.Father, MemberAddress.Father });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "高祖母",
                Six = Gender.Female,
                Level = 4,
            });
            right.AddMemberAddress(new[] { MemberAddress.Father, MemberAddress.Father, MemberAddress.Father, MemberAddress.Mother });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "外高祖父",
                Six = Gender.Male,
                Level = 4,
            });
            left.AddMemberAddress(new[] { MemberAddress.Mother, MemberAddress.Father, MemberAddress.Father, MemberAddress.Father });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "外高祖母",
                Six = Gender.Female,
                Level = 4,
            });
            right.AddMemberAddress(new[] { MemberAddress.Mother, MemberAddress.Father, MemberAddress.Father, MemberAddress.Mother });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "曾祖父",
                Six = Gender.Male,
                Level = 3,
            });
            left.AddMemberAddress(new[] { MemberAddress.Father, MemberAddress.Father, MemberAddress.Father });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "曾祖母",
                Six = Gender.Female,
                Level = 3,
            });
            right.AddMemberAddress(new[] { MemberAddress.Father, MemberAddress.Father, MemberAddress.Mother });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "外曾祖父",
                Six = Gender.Male,
                Level = 3,
            });
            left.AddMemberAddress(new[] { MemberAddress.Mother, MemberAddress.Father, MemberAddress.Father });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "外曾祖母",
                Six = Gender.Female,
                Level = 3,
            });
            right.AddMemberAddress(new[] { MemberAddress.Mother, MemberAddress.Father, MemberAddress.Mother });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "爺爺",
                Six = Gender.Male,
                Level = 2,
            });
            left.AddMemberAddress(new[] { MemberAddress.Father, MemberAddress.Father });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "奶奶",
                Six = Gender.Female,
                Level = 2,
            });
            right.AddMemberAddress(new[] { MemberAddress.Father, MemberAddress.Mother });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "外公",
                Six = Gender.Male,
                Level = 2,
            });
            left.AddMemberAddress(new[] { MemberAddress.Mother, MemberAddress.Father });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "外婆",
                Six = Gender.Female,
                Level = 2,
            });
            right.AddMemberAddress(new[] { MemberAddress.Mother, MemberAddress.Mother });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "伯伯",
                Six = Gender.Male,
                Level = 1,
            });
            left.AddMemberAddress(new[] { MemberAddress.Father, MemberAddress.ElderBrother });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "伯娘",
                Six = Gender.Female,
                Level = 1,
            });
            right.AddMemberAddress(new[] { MemberAddress.Father, MemberAddress.ElderBrother, MemberAddress.Wife });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "姑媽",
                Six = Gender.Female,
                Level = 1,
            });
            right.AddMemberAddress(new[] { MemberAddress.Father, MemberAddress.ElderSister });
            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "姑丈",
                Six = Gender.Male,
                Level = 1,
            });
            left.AddMemberAddress(new[] { MemberAddress.Father, MemberAddress.ElderSister, MemberAddress.Husband });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "姑姐",
                Six = Gender.Female,
                Level = 1,
            });
            right.AddMemberAddress(new[] { MemberAddress.Father, MemberAddress.YoungerSister });
            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "姑丈",
                Six = Gender.Male,
                Level = 1,
            });
            left.AddMemberAddress(new[] { MemberAddress.Father, MemberAddress.YoungerSister, MemberAddress.Husband });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "父亲",
                Six = Gender.Male,
                Level = 1,
            });
            left.AddMemberAddress(new[] { MemberAddress.Father });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "母亲",
                Six = Gender.Female,
                Level = 1,
            });
            right.AddMemberAddress(new[] { MemberAddress.Father, MemberAddress.Wife });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "叔叔",
                Six = Gender.Male,
                Level = 1,
            });
            left.AddMemberAddress(new[] { MemberAddress.Father, MemberAddress.YoungerBrother });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "婶婶",
                Six = Gender.Female,
                Level = 1,
            });
            right.AddMemberAddress(new[] { MemberAddress.Father, MemberAddress.YoungerBrother, MemberAddress.Wife });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "家公",
                Six = Gender.Male,
                Level = 1,
            });
            left.AddMemberAddress(new[] { MemberAddress.Husband, MemberAddress.Father });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "家婆",
                Six = Gender.Female,
                Level = 1,
            });
            right.AddMemberAddress(new[] { MemberAddress.Husband, MemberAddress.Mother });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "岳父",
                Six = Gender.Male,
                Level = 1,
            });
            left.AddMemberAddress(new[] { MemberAddress.Wife, MemberAddress.Father });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "岳母",
                Six = Gender.Female,
                Level = 1,
            });
            right.AddMemberAddress(new[] { MemberAddress.Wife, MemberAddress.Mother });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "舅舅",
                Six = Gender.Male,
                Level = 1,
            });
            left.AddMemberAddress(new[] { MemberAddress.Mother, MemberAddress.ElderBrother });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "舅母",
                Six = Gender.Female,
                Level = 1,
            });
            right.AddMemberAddress(new[] { MemberAddress.Mother, MemberAddress.ElderBrother, MemberAddress.Wife });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "姨妈",
                Six = Gender.Female,
                Level = 1,
            });
            right.AddMemberAddress(new[] { MemberAddress.Mother, MemberAddress.ElderSister, MemberAddress.Wife });
            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "姨丈",
                Six = Gender.Male,
                Level = 1,
            });
            right.AddMemberAddress(new[] { MemberAddress.Mother, MemberAddress.ElderSister, MemberAddress.Husband });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "哥哥",
                Six = Gender.Male,
            });
            left.AddMemberAddress(new[] { MemberAddress.ElderBrother });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "嫂子",
                Six = Gender.Female,
            });
            right.AddMemberAddress(new[] { MemberAddress.ElderBrother, MemberAddress.Wife });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "姐姐",
                Six = Gender.Female,
            });
            right.AddMemberAddress(new[] { MemberAddress.ElderSister, });
            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "姐夫",
                Six = Gender.Male,
            });
            left.AddMemberAddress(new[] { MemberAddress.ElderSister, MemberAddress.Husband });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "弟弟",
                Six = Gender.Male,
            });
            left.AddMemberAddress(new[] { MemberAddress.YoungerBrother });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "弟妹",
                Six = Gender.Female,
            });
            right.AddMemberAddress(new[] { MemberAddress.YoungerBrother, MemberAddress.Wife });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "妹妹",
                Six = Gender.Female,
            }); right.AddMemberAddress(new[] { MemberAddress.YoungerSister });
            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "妹夫",
                Six = Gender.Male,
            });
            left.AddMemberAddress(new[] { MemberAddress.YoungerSister, MemberAddress.Husband });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "堂哥",
                Six = Gender.Male,
            });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "堂嫂",
                Six = Gender.Female,
            });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "堂姐",
                Six = Gender.Female,
            });
            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "堂姐夫",
                Six = Gender.Male,
            });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "堂弟",
                Six = Gender.Male,
            });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "堂弟妹",
                Six = Gender.Female,
            });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "堂妹",
                Six = Gender.Female,
            });
            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "堂妹夫",
                Six = Gender.Male,
            });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "表哥",
                Six = Gender.Male,
            });
            roleId = IdentityGenerator.Next(roleId);
            contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "表嫂",
                Six = Gender.Female,
            });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "表姐",
                Six = Gender.Female,
            });
            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "表姐夫",
                Six = Gender.Male,
            });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "表弟",
                Six = Gender.Male,
            });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "表弟妹",
                Six = Gender.Female,
            });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "表妹",
                Six = Gender.Female,
            });
            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "表妹夫",
                Six = Gender.Male,
            });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "儿子",
                Six = Gender.Male,
                Level = -1,
            });
            left.AddMemberAddress(new[] { MemberAddress.Son });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "儿媳",
                Six = Gender.Female,
                Level = -1,
            });
            right.AddMemberAddress(new[] { MemberAddress.Son, MemberAddress.Wife });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "女儿",
                Six = Gender.Female,
                Level = -1,
            });
            right.AddMemberAddress(new[] { MemberAddress.Daughter });
            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "女婿",
                Six = Gender.Male,
                Level = -1,
            });
            left.AddMemberAddress(new[] { MemberAddress.Daughter, MemberAddress.Husband });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "侄子",
                Six = Gender.Male,
                Level = -1,
                Remark = "称兄弟的儿子",
            });
            left.AddMemberAddress(new[] { MemberAddress.YoungerBrother | MemberAddress.ElderBrother, MemberAddress.Son });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "侄子媳",
                Six = Gender.Female,
                Level = -1,
            });
            right.AddMemberAddress(new[] { MemberAddress.YoungerBrother | MemberAddress.ElderBrother, MemberAddress.Son, MemberAddress.Wife });

            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "侄女",
                Six = Gender.Female,
                Level = -1,
                Remark = "称兄弟的女儿",
            });
            right.AddMemberAddress(new[] { MemberAddress.YoungerBrother | MemberAddress.ElderBrother, MemberAddress.Daughter });
            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "侄女婿",
                Six = Gender.Male,
                Level = -1,
            });
            left.AddMemberAddress(new[] { MemberAddress.YoungerBrother | MemberAddress.ElderBrother, MemberAddress.Daughter, MemberAddress.Husband });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "外甥子",
                Six = Gender.Male,
                Level = -1,
                Remark = "男性称姊妹的儿子",
            });
            left.AddMemberAddress(new[] { MemberAddress.YoungerSister | MemberAddress.ElderSister, MemberAddress.Son });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "外甥子媳",
                Six = Gender.Female,
                Level = -1,
            });
            right.AddMemberAddress(new[] { MemberAddress.YoungerSister | MemberAddress.ElderSister, MemberAddress.Son, MemberAddress.Wife });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "外甥女",
                Six = Gender.Female,
                Level = -1,
                Remark = "男性称姊妹的女儿",
            });
            right.AddMemberAddress(new[] { MemberAddress.YoungerSister | MemberAddress.ElderSister, MemberAddress.Daughter });
            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "外甥女婿",
                Six = Gender.Male,
                Level = -1,
            });
            left.AddMemberAddress(new[] { MemberAddress.YoungerSister | MemberAddress.ElderSister, MemberAddress.Daughter, MemberAddress.Wife });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "姨甥子",
                Six = Gender.Male,
                Level = -1,
                Remark = "女性称姊妹的儿子",
            });
            left.AddMemberAddress(new[] { MemberAddress.YoungerSister | MemberAddress.ElderSister, MemberAddress.Son });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "姨甥子媳",
                Six = Gender.Female,
                Level = -1,
            });
            right.AddMemberAddress(new[] { MemberAddress.YoungerSister | MemberAddress.ElderSister, MemberAddress.Son, MemberAddress.Wife });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "姨甥女",
                Six = Gender.Female,
                Level = -1,
                Remark = "女性称姊妹的女儿",
            });
            right.AddMemberAddress(new[] { MemberAddress.YoungerSister | MemberAddress.ElderSister, MemberAddress.Daughter });
            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "姨甥女婿",
                Six = Gender.Male,
                Level = -1,
            });
            left.AddMemberAddress(new[] { MemberAddress.YoungerSister | MemberAddress.ElderSister, MemberAddress.Daughter, MemberAddress.Husband });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "孙子",
                Six = Gender.Male,
                Level = -2,
            });
            left.AddMemberAddress(new[] { MemberAddress.Son, MemberAddress.Son });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "孙儿媳",
                Six = Gender.Female,
                Level = -2,
            });
            right.AddMemberAddress(new[] { MemberAddress.Son, MemberAddress.Son, MemberAddress.Wife });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "孙女婿",
                Six = Gender.Male,
                Level = -2,
            });
            left.AddMemberAddress(new[] { MemberAddress.Son, MemberAddress.Daughter, MemberAddress.Husband });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "孙女",
                Six = Gender.Female,
                Level = -2,
            });
            right.AddMemberAddress(new[] { MemberAddress.Son, MemberAddress.Daughter });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "外孙子",
                Six = Gender.Male,
                Level = -2,
            }); left.AddMemberAddress(new[] { MemberAddress.Daughter, MemberAddress.Son });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "外孙子媳",
                Six = Gender.Female,
                Level = -2,
            });
            right.AddMemberAddress(new[] { MemberAddress.Daughter, MemberAddress.Son, MemberAddress.Wife });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "外孙女婿",
                Six = Gender.Male,
                Level = -2,
            });
            left.AddMemberAddress(new[] { MemberAddress.Daughter, MemberAddress.Daughter, MemberAddress.Husband });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "外孙女",
                Six = Gender.Female,
                Level = -2,
            });
            left.AddMemberAddress(new[] { MemberAddress.Daughter, MemberAddress.Daughter });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "曾孙",
                Six = Gender.Male,
                Level = -3,
            });
            left.AddMemberAddress(new[] { MemberAddress.Son, MemberAddress.Son, MemberAddress.Son });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "曾孙媳",
                Six = Gender.Female,
                Level = -3,
            });
            right.AddMemberAddress(new[] { MemberAddress.Son, MemberAddress.Son, MemberAddress.Son, MemberAddress.Wife });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "曾孙女婿",
                Six = Gender.Male,
                Level = -3,
            });
            left.AddMemberAddress(new[] { MemberAddress.Son, MemberAddress.Son, MemberAddress.Daughter, MemberAddress.Husband });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "曾孙女",
                Six = Gender.Female,
                Level = -3,
            });
            right.AddMemberAddress(new[] { MemberAddress.Son, MemberAddress.Son, MemberAddress.Daughter });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "玄孙",
                Six = Gender.Male,
                Level = -4,
            }); left.AddMemberAddress(new[] { MemberAddress.Son, MemberAddress.Son, MemberAddress.Son, MemberAddress.Son });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "玄孙媳",
                Six = Gender.Female,
                Level = -4,
            }); right.AddMemberAddress(new[] { MemberAddress.Son, MemberAddress.Son, MemberAddress.Son, MemberAddress.Son, MemberAddress.Wife });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            roleId = IdentityGenerator.Next(roleId);
            left = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "玄孙女婿",
                Six = Gender.Male,
                Level = -4,
            });
            left.AddMemberAddress(new[] { MemberAddress.Son, MemberAddress.Son, MemberAddress.Son, MemberAddress.Daughter, MemberAddress.Husband });
            roleId = IdentityGenerator.Next(roleId);
            right = contactRole.Add(new FamilyRole(crate)
            {
                ID = roleId,
                Name = "玄孙女",
                Six = Gender.Female,
                Level = -4,
            }); right.AddMemberAddress(new[] { MemberAddress.Son, MemberAddress.Son, MemberAddress.Son, MemberAddress.Daughter });
            relationId = IdentityGenerator.Next(relationId);
            relation = FamilyRelation.Create(crate, left, right);
            relation.ID = relationId;
            contactRelation.Add(relation);

            //   */

            #endregion MyRegion

            try
            {
                EFUnitOfWork unit = new EFUnitOfWork(dbcontext);
                unit.Commit();
            }
            catch (Exception e)
            {
                throw;
            }

            Console.WriteLine("1");
        }

        [Test()]
        public void TestAlert()
        {
            MainBoundedContext dbcontext = new MainBoundedContext();

            var crate = new testCreate();
            var messageEntitys = dbcontext.Set<MessageEntity>();
            var mesage = new MessageEntity(crate) { Content = "test alert", Subject = "alert" };

            messageEntitys.Add(mesage);
            mesage.Logs = new List<MessageLogEntity>()
            {
               new MailMessageLogEntity(crate) {To = "11@11.com"},
               new PhoneMessageLogEntity(crate) {PhoneNumber = "11212121"}
            };
            try
            {
                EFUnitOfWork unit = new EFUnitOfWork(dbcontext);
                unit.Commit();
            }
            catch (Exception e)
            {
                throw;
            }

            var messges = messageEntitys.AsNoTracking().ToArray();
            Console.WriteLine(messges);
        }
    }
}