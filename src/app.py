import csv
from collections import Counter
from flask import Flask, request

from src.api.v1.list_api import app

# csv_path = r'C:\Users\angel\OneDrive\Escritorio\LifeEnsure\src\incidentesviales_noviembre22.csv'

# colonias = []

# with open(csv_path) as f:
#     reader = csv.reader(f)
#     next(reader)  # Saltar la primera fila de encabezados
#     for row in reader:
#         colonias.append(row[9])

# frecuencia_colonias = Counter(colonias)
# colonia_mas_comun = frecuencia_colonias.most_common(1)[0][0]

# @app.route('/get_colonia/<colonia>', methods=['GET'])
# def get_colonia_accidentes(colonia):
#     colonia = colonia.upper()
#     accidentes_colonia = [row for row in colonias if row.upper() == colonia]
#     cantidad_accidentes = len(accidentes_colonia)
#     return f"La colonia {colonia} tiene {cantidad_accidentes} accidentes."

# @app.route('/colonia_mas_comun', methods=['GET'])
# def get_colonia_mas_comun():
#     return f"La colonia que más accidentes tiene es: {colonia_mas_comun}"

if __name__ == '__main__':
    app.run(debug=True)
