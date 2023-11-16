
//진급 범위 Class
public class Rank
{

    public int month;
    public string rank { get; set; }

    public Rank(int initialMonth)
    {
        month = initialMonth;
    }

    public void IncreaseMonth()
    {
        month++;
    }

    public void SetRank()
    {
        if (month >= 1 && month <= 2)
        {
            rank = "이등병";
        }
        else if (month >= 3 && month <= 8)
        {
             rank = "일병";
        }
        else if (month >= 9 && month <= 14)
        {
            rank =  "상병";
        }
        else if (month >= 15 && month <= 18)
        {
             rank =  "병장";
        }
        else
        {
             rank =  "민간인";
        }
    }
}