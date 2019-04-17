namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testrelation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PostContents", "Relationid", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Relationid", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Relationid", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.PostContents", "Relationid", c => c.Int(nullable: false));
        }
    }
}
