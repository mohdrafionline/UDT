namespace SmartAdminMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editstaff : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Staffs", "W9Form", c => c.String());
            AlterColumn("dbo.Staffs", "StaffPhoto", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Staffs", "StaffPhoto", c => c.Binary());
            AlterColumn("dbo.Staffs", "W9Form", c => c.Binary());
        }
    }
}
