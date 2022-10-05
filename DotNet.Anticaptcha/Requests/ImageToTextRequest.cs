using System.IO;
using DotNet.Anticaptcha.Enums;
using DotNet.Anticaptcha.Internal.Helpers;
using DotNet.Anticaptcha.Requests.Abstractions;

namespace DotNet.Anticaptcha.Requests
{
    
    /// <summary>
    /// Solve image captcha.
    /// Post an image body and receive text from it.
    /// Text can only contain digits, letters, special characters and a space.
    /// GIF animations are supported, up to 500kb.
    /// Custom captchas like "find a cat in this series of images and enter its number" are not supported.
    /// </summary>
    public class ImageToTextRequest : CaptchaRequest
    {
        /// <summary>
        /// [Required]
        /// File body encoded in base64. Make sure to send it without line breaks.
        /// Do not include 'data:image/png,' or similar tags, only clean base64!
        ///
        /// !IMPORTANT
        /// Is written to while FilePath is set. The file from the file path is read and the body is written here.
        /// </summary>
        public string BodyBase64 { internal get; set; }
        
        /// <summary>
        /// [Optional]
        /// false - no requirements
        /// true - requires workers to enter an answer with at least one "space". If there are no spaces, they will skip the task, so use it with caution.
        /// </summary>
        public bool Phrase { internal get; set; } = false;
        
        
        /// <summary>
        /// [Optional]
        /// false - no requirements
        /// true - workers see a special mark indicating that the answer must be entered with case sensitivity.
        /// </summary>
        public bool Case { internal get; set; } = false;
        
        
        /// <summary>
        /// [Optional]
        /// 0 - no requirements
        /// 1 - only numbers are allowed
        /// 2 - any letters are allowed except numbers
        /// </summary>
        public NumericOption Numeric { internal get; set; } = NumericOption.NoRequirements;
        
        
        /// <summary>
        /// [Optional]
        /// false - no requirements
        /// true - workers see a special mark telling them the answer must be calculated
        /// </summary>
        public int Math { internal get; set; } = 0;
        
        
        /// <summary>
        /// [Optional]
        /// 0 - no requirements
        /// >1 - defines minimum length of the answer
        /// </summary>
        public int MinLength { internal get; set; } = 0;
        
        
        /// <summary>
        /// [Optional]
        ///  0 - no requirements
        ///  >1 - defines maximum length of the answer
        /// </summary>
        public int MaxLength { internal get; set; } = 0;
        
        
        /// <summary>
        /// [Optional]
        /// Additional comments for workers like "enter red text".
        /// The result is not guaranteed and is totally up to the worker.
        /// </summary>
        public string Comment { internal get; set; }
        
        /// <summary>
        /// [Optional]
        /// Optional parameter to distinguish source of image captchas in spending statistics.
        /// </summary>
        public string WebsiteUrl { internal get; set; }

        /// <summary>
        /// [Optional]
        /// When set, the content from file in the path si written into BodyBod64.
        /// </summary>
        public string FilePath
        {
            set
            {
                if (File.Exists(value))
                {
                    BodyBase64 = StringHelper.ImageFileToBase64String(value);
                }
            }
        }
    }
}