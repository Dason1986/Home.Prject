using DomainModel;
using NUnit.Framework;
using Repository;
using System;
using Library.ComponentModel.Model;
using Library;

namespace RepositoryTest
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		public void TestCase ()
		{
			MainBoundedContext dbcontext = new MainBoundedContext ();
			var roleId = Guid.Parse ("00f73871-afe7-431a-a9ec-df44b1dcb736");
			var contactRole = dbcontext.Set<FamilyRole> ();
			contactRole.Add (new FamilyRole () {
				ID = roleId,
				Created = DateTime.Now,
				Name = "高祖父",
				Six= Gender
    .Male,
				Level = 4,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "高祖母",
				Six= Gender
    .Female,
				Level = 4,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "外高祖父",
				Six= Gender
    .Male,
				Level = 4,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "外高祖母",
				Six= Gender
    .Female,
				Level = 4,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});

			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "曾祖父",
				Six= Gender
    .Male,
				Level = 3,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "曾祖母",
				Six= Gender
    .Female,
				Level = 3,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "外曾祖父",
				Six= Gender
    .Male,
				Level = 3,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "外曾祖母",
				Six= Gender
    .Female,
				Level = 3,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});

			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "爺爺",
				Six= Gender
    .Male,
				Level = 2,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "奶奶",
				Six= Gender
    .Female,
				Level = 2,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "外公",
				Six= Gender
    .Male,
				Level = 2,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "外婆",
				Six= Gender
    .Female,
				Level = 2,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});

			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "伯伯",
				Six= Gender
    .Male,
				Level = 1,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "伯娘",
				Six= Gender
    .Female,
				Level = 1,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "姑媽",
				Six= Gender
    .Female,
				Level = 1,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "姑丈",
				Six= Gender
    .Male,
				Level = 1,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "父亲",
				Six= Gender
    .Male,
				Level = 1,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "母亲",
				Six= Gender
    .Female,
				Level = 1,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "叔叔",
				Six= Gender
    .Male,
				Level = 1,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "婶婶",
				Six= Gender
    .Female,
				Level = 1,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "姑姐",
				Six= Gender
    .Female,
				Level = 1,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});

	 
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "岳父",
				Six= Gender
    .Male,
				Level = 1,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "岳母",
				Six= Gender
    .Female,
				Level = 1,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "舅舅",
				Six= Gender
    .Male,
				Level = 1,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "舅母",
				Six= Gender
    .Female,
				Level = 1,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "姨妈",
				Six= Gender
    .Female,
				Level = 1,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "姨丈",
				Six= Gender
    .Male,
				Level = 1,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});


			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "哥哥",
				Six= Gender
    .Male,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "嫂",
				Six= Gender
    .Female,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "姐姐",
				Six= Gender
    .Female,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "姐夫",
				Six= Gender
    .Male,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "弟弟",
				Six= Gender
    .Male,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});

			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "弟妹",
				Six= Gender
    .Female,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "妹妹",
				Six= Gender
    .Female,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "妹夫",
				Six= Gender
    .Male,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});

			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "堂哥",
				Six= Gender
    .Male,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "堂嫂",
				Six= Gender
    .Female,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "堂姐",
				Six= Gender
    .Female,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "堂姐夫",
				Six= Gender
    .Male,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "堂弟",
				Six= Gender
    .Male,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "堂弟妹",
				Six= Gender
    .Female,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "堂妹",
				Six= Gender
    .Female,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "堂妹夫",
				Six= Gender
    .Male,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "表哥",
				Six= Gender
    .Male,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "表嫂",
				Six= Gender
    .Female,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "表姐",
				Six= Gender
    .Female,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "表姐夫",
				Six= Gender
    .Male,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "表弟",
				Six= Gender
    .Male,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "表弟妹",
				Six= Gender
    .Female,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "表妹",
				Six= Gender
    .Female,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "表妹夫",
				Six= Gender
    .Male,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});


			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "儿子",
				Six= Gender
    .Male,
				Level = -1,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "儿媳",
				Six= Gender
    .Female,
				Level = -1,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});

			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "女儿",
				Six= Gender
    .Female,
				Level = -1,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});	
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "女婿",
				Six= Gender
    .Male,
				Level = -1,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});

			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "侄子",
				Six= Gender
    .Male,
				Level = -1,
				Remark = "称兄弟的儿子",
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "侄子媳",
				Six= Gender
    .Female,
				Level = -1,			 
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "侄女",
				Six= Gender
    .Female,
				Level = -1,
				Remark = "称兄弟的女儿",
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "侄女婿",
				Six= Gender
    .Male,
				Level = -1,
			 
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "外甥子",
				Six= Gender
    .Male,
				Level = -1,
				Remark = "男性称姊妹的儿子",
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "外甥子媳",
				Six= Gender
    .Female,
				Level = -1,
				Remark = "男性称姊妹的女儿",
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "外甥女",
				Six= Gender
    .Female,
				Level = -1,
				Remark = "男性称姊妹的女儿",
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "外甥女婿",
				Six= Gender
    .Male,
				Level = -1,
				Remark = "男性称姊妹的女儿",
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "姨甥子",
				Six= Gender
    .Male,
				Level = -1,
				Remark = "女性称姊妹的儿子",
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "姨甥子媳",
				Six= Gender
    .Female,
				Level = -1,
				Remark = "女性称姊妹的儿子",
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "姨甥女",
				Six= Gender
    .Female,
				Level = -1,
				Remark = "女性称姊妹的女儿",
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "姨甥女婿",
				Six= Gender
    .Male,
				Level = -1,			 
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});


			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "孙子",
				Six= Gender
    .Male,
				Level = -2,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "孙女",
				Six= Gender
    .Female,
				Level = -2,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "外孙子",
				Six= Gender
    .Male,
				Level = -2,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "外孙女",
				Six= Gender
    .Female,
				Level = -2,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});

			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "曾孙",
				Six= Gender
    .Male,
				Level = -3,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "曾孙女",
				Six= Gender
    .Female,
				Level = -3,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});

			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "玄孙",
				Six= Gender
    .Male,
				Level = -4,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
			contactRole.Add (new FamilyRole () {
				ID = IdentityGenerator.Next(roleId),
				Created = DateTime.Now,
				Name = "玄孙女",
				Six= Gender
    .Female,
				Level = -4,
				CreatedBy = "sqlscript",
				StatusCode =StatusCode.Enabled
			});
     
			/*
                        contactRole.Add(new ContactRole() { ID = Guid.Parse("10bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "来孙", CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });
                        contactRole.Add(new ContactRole() { ID = Guid.Parse("10bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "来孙女", CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });

                        contactRole.Add(new ContactRole() { ID = Guid.Parse("10bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "晜孙", CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });
                        contactRole.Add(new ContactRole() { ID = Guid.Parse("10bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "晜孙女", CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });
            */


			/*
            var contactRelation = dbcontext.Set<ContactRelation>();
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("10bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "夫妻", Line = RelationLine.None, Range = 0, CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("11bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "父子", Line = RelationLine.Paternal, CreatedBy = "sqlscript", Range = 1, StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("12bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "父女", Line = RelationLine.Paternal, CreatedBy = "sqlscript", Range = 1, StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("13bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "母子", Line = RelationLine.Paternal, CreatedBy = "sqlscript", Range = 1, StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("14bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "母女", Line = RelationLine.Paternal, CreatedBy = "sqlscript", Range = 1, StatusCode =StatusCode.Enabled });

            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("15bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "爺孫", Line = RelationLine.Paternal, CreatedBy = "sqlscript", Range = 2, StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("16bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "爺孫女", Line = RelationLine.Paternal, CreatedBy = "sqlscript", Range = 2, StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("17bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "婆孫", Line = RelationLine.Paternal, CreatedBy = "sqlscript", Range = 2, StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("18bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "婆孫女", Line = RelationLine.Paternal, CreatedBy = "sqlscript", Range = 2, StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("19bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "外爺孫", Line = RelationLine.Maternal, CreatedBy = "sqlscript", Range = 2, StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("1Abee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "外爺孫女", Line = RelationLine.Maternal, CreatedBy = "sqlscript", Range = 2, StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("1Bbee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "外婆孫", Line = RelationLine.Maternal, CreatedBy = "sqlscript", Range = 2, StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("1Cbee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "外婆孫女", Line = RelationLine.Maternal, CreatedBy = "sqlscript", Range = 2, StatusCode =StatusCode.Enabled });

            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("1Dbee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "兄弟", Line = RelationLine.Paternal, CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("1ebee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "兄妹", Line = RelationLine.Paternal, CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("1fbee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "姐妹", Line = RelationLine.Paternal, CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("20bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "姐弟", Line = RelationLine.Paternal, CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("21bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "堂兄弟", Line = RelationLine.Paternal, CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("22bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "堂兄妹", Line = RelationLine.Paternal, CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("23bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "堂姐妹", Line = RelationLine.Paternal, CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("24bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "堂姐弟", Line = RelationLine.Paternal, CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("25bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "表兄弟", Line = RelationLine.Maternal, CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("26bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "表兄妹", Line = RelationLine.Maternal, CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("27bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "表姐妹", Line = RelationLine.Maternal, CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("28bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "表姐弟", Line = RelationLine.Maternal, CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });

            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("28bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "伯侄", Line = RelationLine.Maternal, CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });
            contactRelation.Add(new ContactRelation() { ID = Guid.Parse("28bee159-a76e-4864-b151-73082da3cb18"), Created = DateTime.Now, Name = "叔侄", Line = RelationLine.Maternal, CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });
            */

			//var contactProfile = dbcontext.Set<ContactProfile>();
			//contactProfile.Add(new ContactProfile() { ID = Guid.NewGuid(), Created = DateTime.Now, CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });

			//var usres = dbcontext.Set<UserProfile>();
			//usres.Add(new UserProfile() { ID = Guid.NewGuid(), Created = DateTime.Now, CreatedBy = "sqlscript", StatusCode =StatusCode.Enabled });
			EFUnitOfWork unit = new EFUnitOfWork (dbcontext);
			unit.Commit ();
			Console.WriteLine ("1");
		}
	}
}

