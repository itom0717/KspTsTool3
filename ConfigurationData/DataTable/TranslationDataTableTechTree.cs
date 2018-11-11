using System;
using System.Data;

namespace ConfigurationData.DataTable
{
    /// <summary>
    /// 翻訳データベース処理(TechTree)
    /// </summary>
    public class TranslationDataTableTechTree : TranslationDataTable
    {

        /// <summary>
        /// ID
        /// </summary>
        private  static string ColumnNameID  = @"ID";

        /// <summary>
        /// タイトル
        /// </summary>
        private static string ColumnNameTitle  = @"Title";


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TranslationDataTableTechTree() : base( DataType.TechTree )
        {

            // ID
            {
                var column = this.Columns.Add( ColumnNameID , typeof( System.String ) );
                column.DefaultValue = "";
                column.AllowDBNull = false;
                column.SetOrdinal( SetOrdinalCount++ );
            }
            // タイトル
            {
                var column = this.Columns.Add( ColumnNameTitle , typeof( System.String ) );
                column.DefaultValue = "";
                column.AllowDBNull = false;
                column.SetOrdinal( SetOrdinalCount++ );
            }
        }

        /// <summary>
        /// DBに存在するかチェック
        /// </summary>
        public override DataRow[] GetExistsDataRow( string directoryName ,
                                                    Text.TextData textData ,
                                                    Translate.TranslateText translateText )
        {
            Text.TextDataTechTree tData = (Text.TextDataTechTree)textData;

            var where = new System.Text.StringBuilder();
            where.Clear();

            if ( directoryName != null )
            {
                where.Append( String.Format( "{0}='{1}'" ,
                                             ColumnNameDirName ,
                                             this.DoubleSiglQrt( directoryName ) ) );
                where.Append( " AND " );
            }


            where.Append( String.Format( "{0}='{1}'" ,
                                         ColumnNameID ,
                                         this.DoubleSiglQrt( tData.ID ) ) );


            return this.Select( where.ToString() );
        }


        /// <summary>
        /// タイトル等データをセット
        /// </summary>
        public override void SetTitleData( DataRow row ,
                                      Text.TextData textData ,
                                      Translate.TranslateText translateText )
        {
            Text.TextDataTechTree tData = (Text.TextDataTechTree)textData;


            if ( row.RowState == DataRowState.Added || row.RowState == DataRowState.Detached )
            {
                // ID
                this.SetDataValue( row , ColumnNameID , tData.ID );
            }

            // タイトル
            this.SetDataValue( row , ColumnNameTitle , tData.Title );


        }



    }
}
