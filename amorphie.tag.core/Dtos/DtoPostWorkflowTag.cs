using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

    public class DtoPostWorkflow
    {
        public Guid recordId {get;set;}
        public DtoSaveTagRequest? entityData {get;set;}
        public string newStatus {get;set;}=default!;
        public Guid? user {get;set;}
        public Guid? behalfOfUser {get;set;}
        public string  workflowName {get;set;}=default!;
    }
