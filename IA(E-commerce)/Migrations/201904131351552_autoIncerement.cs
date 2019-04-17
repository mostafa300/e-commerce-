namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class autoIncerement : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Relationid", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Relationid", c => c.Int(nullable: false));
        }
    }
}
