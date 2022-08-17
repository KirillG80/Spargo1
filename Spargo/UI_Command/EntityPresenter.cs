using System;

namespace Spargo.UI_Command
{
    public class EntityPresenter<T> where T : DisplayEntity
    {
        public void ShowEntity(T e)
        {
            Console.WriteLine(e.result);
        }
    }
}
