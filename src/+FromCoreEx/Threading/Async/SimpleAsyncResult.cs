#region License
/*
The MIT License

Copyright (c) 2008 Sky Morey

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
namespace System.Threading.Async
{
    /// <summary>
    /// SimpleAsyncResult
    /// </summary>
    internal sealed class SimpleAsyncResult : IAsyncResult
    {
        private readonly object _asyncState;
        private bool _completedSynchronously;
        private volatile bool _isCompleted;

        public SimpleAsyncResult(object asyncState)
        {
            _asyncState = asyncState;
        }

        public void MarkCompleted(bool completedSynchronously, AsyncCallback callback)
        {
            _completedSynchronously = completedSynchronously;
            _isCompleted = true;
            if (callback != null)
                callback(this);
        }

        public object AsyncState
        {
            get { return _asyncState; }
        }

        public WaitHandle AsyncWaitHandle
        {
            get { return null; }
        }

        public bool CompletedSynchronously
        {
            get { return _completedSynchronously; }
        }

        public bool IsCompleted
        {
            get { return _isCompleted; }
        }
    }
}