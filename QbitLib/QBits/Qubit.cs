namespace QBits.QBits
{
    public interface Qubit 
    {

        void h();
        void x();

        bool measure();

        void reset();
    }

}