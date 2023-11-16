
//진급 범위 Class
public class Rank
{

    public int month;string rank { get; set; }

    public Rank(int initialMonth)
    {
        month = initialMonth;
    }

    public void IncreaseMonth()
    {
        month++;
    }

    public string SetRank()
    {
        if (month >= 1 && month <= 2)
        {
            return rank = "이등병";
        }
        else if (month >= 3 && month <= 8)
        {
            return rank = "일병";
        }
        else if (month >= 9 && month <= 14)
        {
            return rank =  "상병";
        }
        else if (month >= 15 && month <= 18)
        {
            return rank =  "병장";
        }
        else
        {
            return rank =  "민간인";
        }
    }
}