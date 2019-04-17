namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateintrelation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PostContents", "Relationid", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Relationid", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Relationid", c => c.String(nullable: false));
            AlterColumn("dbo.PostContents", "Relationid", c => c.String());
        }
    }
}
