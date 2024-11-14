const amqp = require('amqplib');

const queue = 'rmQueue';

async function consumeMessage() {
  try {
    const conn = await amqp.connect('amqp://localhost');
    const channel = await conn.createChannel();

    await channel.assertQueue(queue, { durable: true });

    console.log(`Waiting for messages in ${queue}. Press CTLR+C to exit`);

    channel.consume(queue, (msg) => {
      if (msg !== null) {
        console.log(`Received: ${msg.content.toString()}`);
        channel.ack(msg);
      }
    });

  } catch (err) {
    console.error('Error consuming messages: ', error);
    setTimeout(consume, 5000);
  }
}

consumeMessage();