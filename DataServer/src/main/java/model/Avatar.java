package model;

public class Avatar
{
	private String Owner;
	private String AvatarName;
	private Status Status;
	private int TimeBlock;

	
	public Avatar(String owner, String avatarName, Status status, int timeBlock)
	{
		Owner = owner;
		AvatarName = avatarName;
		Status = status;
		TimeBlock = timeBlock;
	}
	
	public String getAvatarName()
	{
		return AvatarName;
	}
	
	public String getOwner()
	{
		return Owner;
	}
	
	public int getAcademic()
	{
		return Status.GetStatus("Academic");
	}
	
	public int getFinancial()
	{
		return Status.GetStatus("Financial");
	}
	
	public int getHealth()
	{
		return Status.GetStatus("Health");
	}
	
	public int getSocial()
	{
		return Status.GetStatus("Social");
	}

	public int getTimeBlock() {
		return TimeBlock;
	}
}

