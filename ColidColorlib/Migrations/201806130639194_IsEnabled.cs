namespace ColidColorlib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsEnabled : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsEnabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsEnabled");
        }
    }
}
