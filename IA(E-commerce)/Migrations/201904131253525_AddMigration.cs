namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostContents", "Userid", c => c.String());
            AddColumn("dbo.PostContents", "UserPost_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.PostContents", "UserPost_Id");
            AddForeignKey("dbo.PostContents", "UserPost_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostContents", "UserPost_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PostContents", new[] { "UserPost_Id" });
            DropColumn("dbo.PostContents", "UserPost_Id");
            DropColumn("dbo.PostContents", "Userid");
        }
    }
}
