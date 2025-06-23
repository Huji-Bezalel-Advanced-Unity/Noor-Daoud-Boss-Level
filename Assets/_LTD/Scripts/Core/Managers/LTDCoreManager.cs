namespace LTD.Core.Managers
{
    public class LTDCoreManager
    {
         public static LTDCoreManager Instance { get; private set; }
        
         public LTDCoreManager()
         {
             Instance = this;
         }
}
}