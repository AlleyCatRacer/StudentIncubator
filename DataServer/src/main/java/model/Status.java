package model;

import java.util.Hashtable;

public class Status
  {
    private Hashtable<String, Integer> Aspects;
    private int Academic;
    private int Financial;
    private int Health;
    private int Social;
    private final int MAX = 100;
    private final int DEATH = 0;
  
    public Status(int academic, int financial, int health, int social)
    {
      Academic = academic;
      Financial = financial;
      Health = health;
      Social = social;
      Aspects = new Hashtable<String, Integer>();
      Aspects.put("Academic", Academic);
      Aspects.put("Financial", Financial);
      Aspects.put("Health", Health);
      Aspects.put("Social", Social);
    }

    public final int GetStatus(String statusType)
    {
      try
      {
        return Aspects.get(statusType);
      }
      catch (NullPointerException e)
      {
        System.out.println(e);
      }
      return -1;
    }

    public final int[] GetAllStatuses()
    {
      int[] statuses = null;
      try
      {
        int i = 0;
        for (String key : Aspects.keySet())
        {
          statuses[i] = Aspects.get(key);
          i++;
        }
      }
      catch (Exception e)
      {
        System.out.println(e.getMessage());
      }

      return statuses;
    }
  }
