using System.IO;
using AntiCaptchaApi.Net.Enums;
using AntiCaptchaApi.Net.Internal.Helpers;
using AntiCaptchaApi.Net.Models.Solutions;
using AntiCaptchaApi.Net.Requests.Abstractions;
using AntiCaptchaApi.Net.Requests.Abstractions.Interfaces;
using Newtonsoft.Json;

namespace AntiCaptchaApi.Net.Requests
{
    
    /// <summary>
    /// Solve image captcha.
    /// Post an image body and receive text from it.
    /// Text can only contain digits, letters, special characters and a space.
    /// GIF animations are supported, up to 500kb.
    /// Custom captchas like "find a cat in this series of images and enter its number" are not supported.
    /// </summary>
    public class ImageToTextRequest : CaptchaRequest<ImageToTextSolution>, IImageToTextRequest
    {
        /// <summary>
        /// [Required]
        /// File body encoded in base64. Make sure to send it without line breaks.
        /// Do not include 'data:image/png,' or similar tags, only clean base64!
        ///
        /// !IMPORTANT
        /// Is written to while FilePath is set. The file from the file path is read and the body is written here.
        /// </summary>
        
        [JsonProperty("body")]
        public string BodyBase64 { get; set; }
        
        /// <summary>
        /// [Optional]
        /// false - no requirements
        /// true - requires workers to enter an answer with at least one "space". If there are no spaces, they will skip the task, so use it with caution.
        /// </summary>
        public bool? Phrase { get; set; }
        
        
        /// <summary>
        /// [Optional]
        /// false - no requirements
        /// true - workers see a special mark indicating that the answer must be entered with case sensitivity.
        /// </summary>
        public bool? Case { get; set; }
        
        
        /// <summary>
        /// [Optional]
        /// 0 - no requirements
        /// 1 - only numbers are allowed
        /// 2 - any letters are allowed except numbers
        /// </summary>
        public NumericOption? Numeric { get; set; }
        
        
        /// <summary>
        /// [Optional]
        /// false - no requirements
        /// true - workers see a special mark telling them the answer must be calculated
        /// </summary>
        public bool? Math { get; set; }
        
        
        /// <summary>
        /// [Optional]
        /// 0 - no requirements
        /// >1 - defines minimum length of the answer
        /// </summary>
        public int? MinLength { get; set; }
        
        
        /// <summary>
        /// [Optional]
        ///  0 - no requirements
        ///  >1 - defines maximum length of the answer
        /// </summary>
        public int? MaxLength { get; set; }
        
        
        /// <summary>
        /// [Optional]
        /// Additional comments for workers like "enter red text".
        /// The result is not guaranteed and is totally up to the worker.
        /// </summary>
        public string Comment { get; set; }
        
        /// <summary>
        /// [Optional]
        /// Optional parameter to distinguish source of image captchas in spending statistics.
        /// </summary>
        [JsonProperty("websiteURL")]
        public string WebsiteUrl { get; set; }

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