namespace IA_E_commerce_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateValueRate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Feedbacks", "Rate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feedbacks", "Rate", c => c.Single(nullable: false));
        }
    }
}
