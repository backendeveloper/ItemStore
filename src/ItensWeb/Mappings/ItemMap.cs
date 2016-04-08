using FluentNHibernate.Mapping;
 
public class ItemMap : ClassMap<Item>
{
    public ItemMap()
    {
        Id(item => item.Id).GeneratedBy.Increment();
        Map(item => item.Name);
        Table("itens");
    }
}