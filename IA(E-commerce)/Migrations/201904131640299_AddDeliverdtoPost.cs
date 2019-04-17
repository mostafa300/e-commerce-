namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeliverdtoPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostContents", "delivered", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostContents", "delivered");
        }
    }
}
