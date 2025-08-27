namespace SimpleTracker.DAL;

public class ActivityDal
{
    private ISqlDataAccess SqlDataAccess { get; set; }

    public ActivityDal(ISqlDataAccess sqlDataAccess)
    {
        SqlDataAccess = sqlDataAccess;
    }

    public void GetActivity(int id)
    {
        
    }
}
