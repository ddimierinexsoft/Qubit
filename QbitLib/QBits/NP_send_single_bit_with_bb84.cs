namespace QBits.QBits
{
    public static partial class NP
    {
         
         
        public static ((bool sentMessage, bool senderBasis), (bool receivedMessage, bool receiverBasis)) send_single_bit_with_bb84(bool message, QuantumDevice senderDevice, QuantumDevice receiverDevice)
        {
            var senderBasis = sample_random_bit(senderDevice, StadardBasis);
            var receiverBasis = sample_random_bit(receiverDevice, StadardBasis);

            return senderDevice.using_qubit(q =>
            {
                prepare_message_qubit(message, senderBasis, q);
                //QBIT Sending
                var receiverResult = measure_message_qubit(receiverBasis, q, StadardBasis);
                return ((message, senderBasis), (receiverResult, receiverBasis));
            });
        }
         
    }

}