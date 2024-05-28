namespace MartenDbVerify
{
    public class ProcessAggregator
    {
        public Guid Id { get;private set; }
        public string? Data { get; private set; }

        private int _currentStep;
       
        public void CompleteStep1(string data)
        {
            if (_currentStep != 0) throw new InvalidOperationException("Its not Step 1");

            Apply(new Step1Completed(data));
        }

        public void CompleteStep2(string data) 
        { 
            if (_currentStep != 1) throw new InvalidOperationException("Its not step 2");

            Apply(new Step2Completed(data));
        }

        public void CompleteStep3(string data)
        {
            if (_currentStep != 2) throw new InvalidOperationException("Its not step 3");

            Apply(new Step3Completed(data));
        }

        public void CompleteStep4(string data)
        {
            if (_currentStep != 3) throw new InvalidOperationException("Its not step 4");

            Apply(new Step4Completed(data));
        }

        private void Apply(Step4Completed @events)
        {
            Id = Guid.NewGuid();
            Data = @events.data;
            _currentStep = 4;
        }

        private void Apply(Step3Completed @events)
        {
            Id = Guid.NewGuid();
            Data = @events.data;
            _currentStep = 3;
        }


        private void Apply(Step2Completed @events)
        {
            Id = Guid.NewGuid();
            Data = @events.data;
            _currentStep = 2;
        }

        private void Apply(Step1Completed @events)
        {
            Id=Guid.NewGuid();
            Data = @events.data;
            _currentStep = 1;
        }
    }
}
