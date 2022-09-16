using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Commons
{
    public class HttpResult
    {
         public MessageCode messageCode { get; set; }
        public string message { get; set; }
        public object content { get; set; }
        public HttpResult()
        {

        }
        public HttpResult(MessageCode messageCode)
        {
            this.messageCode = messageCode;
        }
        public HttpResult(MessageCode messageCode,string message)
        {
            this.messageCode = messageCode;
            this.message = message;
        }
        public HttpResult(MessageCode messageCode, string message, object content)
        {
            this.messageCode = messageCode;
            this.message = message;
            this.content = content;
        }
    }
    public enum MessageCode
    {
        [Description("None")]
        None = 0,

        [Description("Success")]
        Success = 200,
        [Description("Unauthorized")]
        Unauthorized = 401,

        [Description("Error")]
        Error = 2,

        [Description("Exception")]
        Exception = 3,

        [Description("Token has been expired")]
        TokenExpired = 9,

        [Description("Invalid Token")]
        TokenInvalid = 10,

        [Description("Function not supported")]
        FunctionNotSupport = 13,

        #region Messgage database

        [Description("Unable access to database")]
        UnableAccessDatabase = 20,

        [Description("MessageCode in database not consistence with defined enums")]
        EnumDataInConsistence = 21,

        [Description("object_type not exists in system")]
        SystemObjectTypeNotExists = 22,

        [Description("MessageCode Enums Duplicated. Please contact administrator")]
        DuplicateMessageCode = 23,

        [Description("Special characters do not allow.")]
        SpecialCharacterNotAllow = 24,
        
        [Description("Type not supported")]
        TypeNotSupported = 26,

        [Description("Object type not supported")]
        ObjectTypeNotSupported = 27,
        #endregion

    }
}