namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableTeamProjects : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Teams", new[] { "IdentUser_Id" });
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            DropColumn("dbo.Teams", "MTid");
            RenameColumn(table: "dbo.Teams", name: "IdentUser_Id", newName: "MTID");
            AlterColumn("dbo.Teams", "MTLID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Teams", "MTID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_id", c => c.Int(nullable: false) );
            CreateIndex("dbo.Teams", "MTLID");
            CreateIndex("dbo.Teams", "MTID");
            CreateIndex("dbo.Teams", "Project_Id");
            AddForeignKey("dbo.Teams", "MTLID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "MTLID", "dbo.AspNetUsers");
            DropIndex("dbo.Teams", new[] { "Project_Id" });
            DropIndex("dbo.Teams", new[] { "MTID" });
            DropIndex("dbo.Teams", new[] { "MTLID" });
            AlterColumn("dbo.Teams", "Project_id", c => c.Int());
            AlterColumn("dbo.Teams", "Project_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Teams", "MTID", c => c.String());
            AlterColumn("dbo.Teams", "MTLID", c => c.String());
            RenameColumn(table: "dbo.Teams", name: "MTID", newName: "IdentUser_Id");
            AddColumn("dbo.Teams", "MTid", c => c.String());
            CreateIndex("dbo.Teams", "Project_Id");
            CreateIndex("dbo.Teams", "IdentUser_Id");
        }
    }
}
