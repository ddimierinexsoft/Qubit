using System;

namespace QBits.QBits
{
    public abstract class QuantumDevice  
    {
        public abstract Basis Basis { get; set; }

        public abstract Qubit allocate_qubit();

        public abstract void deallocate_qubit(Qubit qubit);

        public virtual void using_qubit(Action<Qubit> action)
        {
            var qubit = allocate_qubit();
            try
            {
                action(qubit);
            } 
            finally
            {
                qubit.reset();
                deallocate_qubit(qubit);
            }
        }

        public virtual T using_qubit<T>(Func<Qubit, T> fun)
        {
            var qubit = allocate_qubit();
            try
            {
                return fun(qubit);
            }
            finally
            {
                qubit.reset();
                deallocate_qubit(qubit);
            }
        }

    }

}