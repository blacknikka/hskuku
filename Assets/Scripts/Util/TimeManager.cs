using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Timers;

public class TimeManager
{
    // gameの単位時間（出題感覚）
    public float GameInterval { get; set; } = 1.0f;

    public bool Loop { get; set; } = false;

    private Timer _timer = new Timer();

	public event EventHandler TimeElapsed;

    public TimeManager()
    {
		_timer.Elapsed += (s,e) =>
		{
			TimeElapsed?.Invoke(this, EventArgs.Empty);

			if(Loop)
				_timer.Start();
		};
    }

    // イベント
    public void TimerStart()
    {
        _timer.Start();
    }

    public void TimeStop()
    {
        _timer.Stop();
    }
}
