using System;
using CommonDomain.Persistence;
using Documently.Commands;
using Documently.Domain.Domain;
using MassTransit;

namespace Documently.Domain.CommandHandlers
{
    public class DocumentAssociatedWithCollectionHandler : Consumes<AssociateDocumentWithCollection>.All
    {
        private readonly Func<IRepository> _repository;

        public DocumentAssociatedWithCollectionHandler(Func<IRepository> repository)
        {
            if(repository == null) throw new ArgumentNullException("repository");
            _repository = repository;
        }

        public void Consume(AssociateDocumentWithCollection message)
        {
            var repository = _repository();
            var document = repository.GetById<Document>(message.Id);
            document.AssociateWithCollection(message.CollectionId);
            repository.Save(document, Guid.NewGuid(), null);
        }
    }
}