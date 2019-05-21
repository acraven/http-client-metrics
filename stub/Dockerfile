FROM mhart/alpine-node:12.2
WORKDIR /app

COPY package.json /app
COPY yarn.lock /app
RUN yarn install

COPY . /app

EXPOSE 8080

# Using the exec form of CMD allows SIGINT to be passed from container to app
CMD [ "node", "server.js" ]