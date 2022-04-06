using AutoMapper;
using EventBus.Message.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Orders.Commands.CheckOutOrder;

namespace Ordering.API.EventBusConsumer
{
    //IConsumer comes from Mass Transit, called whenever any event is adding to rabbitMQ
    //RabbitMQ message broker
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly ILogger<BasketCheckoutConsumer> logger;

        public BasketCheckoutConsumer(IMapper _mapper, IMediator _mediator, ILogger<BasketCheckoutConsumer> _logger)
        {
            mapper = _mapper;
            mediator = _mediator;
            logger = _logger;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            CheckoutOrderCommand command = mapper.Map<CheckoutOrderCommand>(context.Message);
            int result = await mediator.Send(command);
            logger.LogInformation("BasketCheckoutEvent consumed successfully. Created Order Id : {newOrderId}", result);
        }
    }
}
