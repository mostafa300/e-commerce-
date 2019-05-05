namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSchecduleToPost1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            AlterColumn("dbo.PostContents", "Price", c => c.Single());
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Teams", "Project_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            AlterColumn("dbo.Teams", "Project_id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.PostContents", "Price", c => c.Single(nullable: false));
            CreateIndex("dbo.Teams", "Project_Id");
        }
    }
}
