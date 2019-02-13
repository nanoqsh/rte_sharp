using RTE.OBJReader.Exceptions;

namespace RTE.OBJReader.MTLFormat
{
    public class Comment : MTLType
    {
        public readonly string Text;
        
        public Comment(string[] args) : base(args)
        {
            if (args.Length != 1)
                throw new ArgsAmountException(1, args.Length);
            
            Text = args[0];
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
