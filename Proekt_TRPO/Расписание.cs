//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proekt_TRPO
{
    using System;
    using System.Collections.Generic;
    
    public partial class Расписание
    {
        public int Id { get; set; }
        public System.TimeSpan Время { get; set; }
        public string Кабинет { get; set; }
        public Nullable<int> IdГруппы { get; set; }
        public Nullable<int> IdПредмета { get; set; }
        public Nullable<int> IdПреподавателя { get; set; }
        public string День_недели { get; set; }
    
        public virtual Группы Группы { get; set; }
        public virtual Предметы Предметы { get; set; }
        public virtual Преподаватели Преподаватели { get; set; }
    }
}
