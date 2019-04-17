namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Images", "ImageValue", c => c.Binary(nullable: false));
            
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PostContents", "Title", c => c.String());
            AlterColumn("dbo.Images", "ImageValue", c => c.Binary());
        }
    }
}
