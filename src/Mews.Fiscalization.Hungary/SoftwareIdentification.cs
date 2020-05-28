namespace Mews.Fiscalization.Hungary
{
    public sealed class SoftwareIdentification
    {
        public SoftwareIdentification(string id, string name, string operation, string mainVersion, string developerName, string developerContact, string developerCountry = null, string developerTaxNumber = null)
        {
            Id = id;
            Name = name;
            Operation = operation;
            MainVersion = mainVersion;
            DeveloperName = developerName;
            DeveloperContact = developerContact;
            DeveloperCountry = developerCountry;
            DeveloperTaxNumber = developerTaxNumber;
        }

        public string Id { get; }

        public string Name { get; }

        public string Operation { get; }

        public string MainVersion { get; }

        public string DeveloperName { get; }

        public string DeveloperContact { get; }

        public string DeveloperCountry { get; }

        public string DeveloperTaxNumber { get; }
    }
}