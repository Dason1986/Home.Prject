namespace Home.DomainModel
{
    public enum SourceType
    {
        ServerScand,
        PC,
        Mobile,
        NetWork,
    }
    public enum Gender
    {
        Male,
        Female
    }
    public enum FileLogicType
    {
        File,
        Dir,
    }
    public enum PhotoType
    {
        Graphy,
        Scand
    }
    public enum OffileFileType
    {
        Doc,
        Pdf,
        Execl,
    }
    public enum MemberAddress
    {
        None,
        Husband,
        Wife,
        Father,
        Mother,
        ElderBrother,
        YoungerBrother,
        ElderSister,
        YoungerSister,
        Son,
        Daughter
    }
    public enum FileStatue
    {
        None=0,
        Duplicate=1,
        NotSupport=2,
        Analysis = 100,
    }
    public enum AgeCompare
    {
        Younger=-1,
        None=0,        
        Elder=1,
    }

    public enum SimilarAlgorithm
    {
        GrayHistogram,
        PerceptualHash
    }

    public enum PayTpye
    {
        Cash,
        CreditCard,
        NetPay,
    }

    public enum CurrencyType
    {
        CNY,
        HKD,
        MOP

    }
}