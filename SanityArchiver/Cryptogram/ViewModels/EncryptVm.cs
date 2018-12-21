namespace Cryptogram.ViewModels
{
    public class EncryptVm : DefaultVm
    {
        public EncryptVm() {}

        public EncryptVm(string path) : base(path) {}

        protected override void ActionWithFile(object obj)
        {
            throw new System.NotImplementedException();
        }
    }
}