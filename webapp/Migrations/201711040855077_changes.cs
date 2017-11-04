namespace SmartAdminMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Staffs", "FirstName", c => c.String());
            AlterColumn("dbo.Staffs", "LastName", c => c.String());
            AlterColumn("dbo.Staffs", "Gender", c => c.String());
            AlterColumn("dbo.Staffs", "City", c => c.String());
            AlterColumn("dbo.Staffs", "Region", c => c.String());
            AlterColumn("dbo.Staffs", "HireDate", c => c.DateTime());
            AlterColumn("dbo.Staffs", "TerminationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Staffs", "TerminationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Staffs", "HireDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Staffs", "Region", c => c.Int(nullable: false));
            AlterColumn("dbo.Staffs", "City", c => c.Int(nullable: false));
            AlterColumn("dbo.Staffs", "Gender", c => c.Int(nullable: false));
            AlterColumn("dbo.Staffs", "LastName", c => c.Int(nullable: false));
            AlterColumn("dbo.Staffs", "FirstName", c => c.Int(nullable: false));
        }
    }
}
