namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageValue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ImageValue", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ImageValue");
        }
    }
}
