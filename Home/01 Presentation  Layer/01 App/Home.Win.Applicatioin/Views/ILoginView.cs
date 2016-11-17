using System;
namespace Home.Win.Applicatioin
{
	
	public interface ILoginView:IView
	{
		object TxtUid{get;set;}

		object TxtPwd{get;set;}

		object BtnLogin{get;set;}
	}
}
