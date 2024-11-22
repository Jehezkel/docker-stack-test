# Build Phase

FROM node:22 AS build-stage

#Set  container dir
WORKDIR /app

# copy in parts for layer caching
COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build --prod

