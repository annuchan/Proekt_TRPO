﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TRPOEntities : DbContext
    {
        public TRPOEntities()
            : base("name=TRPOEntities")
        {
        }
    
       
        public virtual DbSet<Группы> Группы { get; set; }
        public virtual DbSet<Должности> Должности { get; set; }
        public virtual DbSet<Предметы> Предметы { get; set; }
        public virtual DbSet<Преподаватели> Преподаватели { get; set; }
        public virtual DbSet<Расписание> Расписание { get; set; }
        public virtual DbSet<Сотрудники> Сотрудники { get; set; }
        public virtual DbSet<Старосты> Старосты { get; set; }
        public virtual DbSet<Студенты> Студенты { get; set; }
        public virtual DbSet<Посещаемость4337> Посещаемость4337 { get; set; }
    }
}
