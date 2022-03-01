package model;

public class Action {

    private String creator;
    private String activity;
    private int academic;
    private int financial;
    private int health;
    private int social;
    private int timeCost;

    public Action(String creator, String activity, int academic, int financial, int health, int social, int timeCost) {
        this.creator = creator;
        this.activity = activity;
        this.academic = academic;
        this.financial = financial;
        this.health = health;
        this.social = social;
        this.timeCost = timeCost;
    }

    public String getCreator() {
        return creator;
    }

    public String getActivity() {
        return activity;
    }

    public int getAcademic() {
        return academic;
    }

    public int getFinancial() {
        return financial;
    }

    public int getHealth() {
        return health;
    }

    public int getSocial() {
        return social;
    }

    public int getTimeCost() {
        return timeCost;
    }
}
