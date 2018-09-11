namespace News.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFieldintable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserPhoto", c => c.Binary());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "UserPhoto");
        }
    }
}
