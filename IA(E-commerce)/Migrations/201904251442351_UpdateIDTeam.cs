namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIDTeam : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            DropPrimaryKey("dbo.Teams");
            AlterColumn("dbo.Teams", "id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Teams", "id");
            CreateIndex("dbo.Teams", "Project_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            DropPrimaryKey("dbo.Teams");
            AlterColumn("dbo.Teams", "Project_id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Teams", "id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Teams", "id");
            CreateIndex("dbo.Teams", "Project_Id");
        }
    }
}
