namespace QBits.QBits
{
    public interface Qubit 
    {

        void h();
        void x();

        bool measure(Basis basis);

        void reset(Basis basis);
    }

}