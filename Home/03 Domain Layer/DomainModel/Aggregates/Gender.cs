namespace Home.DomainModel
{
    public enum SourceType
    {
        ServerScand,
        PC,
        Mobile,
    }
    public enum Gender
    {
        Male,
        Female
    }

    public enum PhotoType
    {
        Graphy,
        Scand
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