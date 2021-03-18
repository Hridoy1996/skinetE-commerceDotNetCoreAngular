using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace  API.Constants
{
  
    public enum ThirdPartyType
    {
        NONE,
        ABACUS,
        INIT_CMS
    }

    public enum PaymentServiceType
    {
        event_registration,
        career_payment,
        legal_payment
    }

    public enum AbacusInterface
    {
        ADDRESS,
        CUSTOMER,
        DOCUMENT,
        DEDICATED_ADDRESS
    }

    public enum EmailTemplate
    {
        confirm_event_registration,
        cancel_event_registration,
        download_file
    }

    public enum PaymentStatusEnum
    {
        MANUAL_PAYMENT,
        INVALID_OR_INCOMPLETE,
        PAYMENT_REQUESTED,
        CANCELLED_BY_CUSTOMER,
        AUTHORISATION_DECLINED,
        UNKNOWN
    }


    public class SkinetConstant
    {

        public const string EN_US = "en-US";
        public const string DE_DE = "de-DE";
        public const string FR_FR = "fr-FR";
        public const string IT_IT = "it-IT";


        public const string ABACUS_USERNAME_KEY = "Username";
        public const string ABACUS_PASSWORD_KEY = "Password";
        public const string ABACUS_MANDANT_KEY = "Mandant";


        public const string ABACUS_VERSION_2017_00 = "address_2017_00";
        public const string ABACUS_CUSTOMER_VERSION_2017_00 = "customer_2017_00";
        public const string ABACUS_DOCUMENT_VERSION_2017_00 = "document_2017_00";


        public const string SALT = "pL���-Q��fĸ<����]:̊�s�\tt\u0004*$߄N";
        public const string DEFAULT_PASSWORD = "1qazZAQ!";

        public const string SKO_CommandQueueName = "SKO_CommandQueue";
   
        public static List<string> SupportedTenants = new List<string>()
        {
            "0456B815-49AF-4AE7-9C05-746D20594074"
        };

      

        private static Dictionary<string, string> SalutationDic = new Dictionary<string, string>
        {
                {"1", "Herr"},
                {"2","Frau"}  ,
                {"8","Herr Prof."}  ,
                {"9","Frau Prof."}  ,
                {"11","Monsieur"} ,
                {"12","Madame"} ,
                {"18","Monsieur le prof."} ,
                {"19","Madame le prof."} ,
                {"21","Signor"} ,
                {"22","Signora"} ,
                {"28","Signor Prof."} ,
                {"29","Signora Prof. ssa"} ,
                {"31","Herr Dr."} ,
                {"32","Frau Dr."} ,
                {"41","Docteur"} ,
                {"51","Dottore"}
        };

    }
}
