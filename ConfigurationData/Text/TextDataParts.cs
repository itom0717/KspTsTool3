using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationData.Text
{
    /// <summary>
    /// パーツ用テキストデータ
    /// </summary>
    public class TextDataParts : TextData
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; } = "";

        /// <summary>
        /// タイトル
        /// </summary>
        public string Title { get; private set; } = "";


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TextDataParts( string name ,
                       string title ,
                       string description )
        {
            this.DataType = DataType.Parts;
            this.Name = name;
            this.Title = title;
            this.TranslateTextList.Add( new Translate.TranslateTextParts( description ) );
        }
    }
}
