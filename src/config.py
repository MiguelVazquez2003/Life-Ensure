import os

class Config:
    DEBUG = False
    SQLALCHEMY_TRACK_MODIFICATIONS = False

class DevelopmentConfig(Config):
    DEBUG = True
    SQLALCHEMY_DATABASE_URI = 'mssql+pyodbc://localhost\\SQLEXPRESS/lifeensure?driver=ODBC+Driver+17+for+SQL+Server'

class ProductionConfig(Config):
    DEBUG = False
    SQLALCHEMY_DATABASE_URI = os.getenv('DATABASE_URI')  # Puedes utilizar una variable de entorno para almacenar la URI en producción

config = {
    'development': DevelopmentConfig,
    'production': ProductionConfig
}
