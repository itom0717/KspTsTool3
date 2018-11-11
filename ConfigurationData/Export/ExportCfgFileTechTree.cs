using System;
using System.Collections.Generic;

namespace ConfigurationData.Export
{
    /// <summary>
    /// ModuleManager用cgfファイル書き出し（TechTree)
    /// </summary>
    class ExportCfgFileTechTree : ExportCfgFile
    {

        /// <summary>
        /// データ書き出し
        /// </summary>
        /// <param name="configurationFolder"></param>
        public override void Export( string directoryName , string savePath , List<ConfigurationFile> cnfigurationFile )
        {
            //ファイル名
            string cfgFilename = Common.File.CombinePath(savePath, directoryName + "_TechTree"+".cfg");

            //格納用
            var exportData = new System.Text.StringBuilder();

            //書き出し用データ作成
            foreach ( ConfigurationFile cfgFile in cnfigurationFile )
            {
                foreach ( Text.TextData textData in cfgFile.TextDataList )
                {
                    if ( textData.DataType == DataType.TechTree )
                    {
                        //パーツ
                        var tData = ( Text.TextDataTechTree ) textData;
                        if ( tData.TranslateTextList.Count >= 1 )
                        {
                            //スペースが含まれている場合は、?に変換
                            string id = tData.ID;
                            id = id.Replace( " " , "?" );

                            if ( directoryName.Equals( VanillaDirectoryName , StringComparison.CurrentCultureIgnoreCase ) )
                            {
                                //(Vanilla
                                exportData.AppendLine( String.Format( "@RDNode:HAS[#id[{0}]]" , id ) );
                            }
                            else
                            {
                                //MOD
                                exportData.AppendLine( String.Format( "@RDNode:HAS[#id[{0}]]:NEEDS[{1}]:FINAL" , id , directoryName ) );
                            }
                            exportData.AppendLine( "{" );

                            exportData.AppendLine( "\t//Title" );
                            exportData.AppendLine( "\t//\t" + tData.Title );
                            exportData.AppendLine( "\t//English Text" );
                            exportData.AppendLine( "\t//\t" + String.Format( @"@description = {0}" , tData.TranslateTextList[0].SourceText ) );
                            exportData.AppendLine( "\t//Japanese Text" );
                            if ( tData.TranslateTextList[0].JapaneseText.Equals( "" ) || tData.TranslateTextList[0].JapaneseText.Equals( tData.TranslateTextList[0].SourceText ) )
                            {
                                exportData.AppendLine( "\t//\t" + @"@description = " );
                            }
                            else
                            {
                                if ( !tData.TranslateTextList[0].Comment.Equals( "" ) )
                                {
                                    exportData.AppendLine( "\t//\t" + tData.TranslateTextList[0].Comment );
                                }
                                exportData.AppendLine( "\t\t" + String.Format( @"@description = {0}" , tData.TranslateTextList[0].JapaneseText ) );
                            }

                            exportData.AppendLine( "}" );
                            exportData.AppendLine( "" );
                        }

                    }
                }
            }


            //データが存在する場合
            if ( exportData.Length > 0 )
            {
                exportData.Insert( 0 , "@TechTree:FINAL {\n" );
                exportData.AppendLine( "}" );
            }



            //データがあればファイル書き出し
            this.DataWrite( cfgFilename , exportData );


        }

    }
}
