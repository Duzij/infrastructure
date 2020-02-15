using System;

namespace Library.ApplicationLayer
{
    public interface IOrderFacade
    {
        void Create(LendRecordCreateDTO record);
    }
}
