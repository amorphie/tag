using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

    public class DtoPostEntityWorkflow
    {
        public Guid recordId {get;set;}
        public DtoEntity? entityData {get;set;}
        [Required]
        public string newStatus {get;set;}=default!;
        public Guid? user {get;set;}
        public Guid? behalfOfUser {get;set;}
        [Required]
        public string  workflowName {get;set;}=default!;
    }
